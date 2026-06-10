import { Typography } from 'antd';
import { useCallback, useEffect, useState } from 'react';
import { createExample, ExampleTable, fetchExamples, type ExampleItem } from '@/entities/example';
import { CreateExampleForm } from '@/features/example/create-item';
import { apiFetch } from '@/shared/api/http';

type HealthResponse = { status: string; service: string };

/**
 * Стартовая страница: health-check и эталонный CRUD примеров.
 */
export function HomePage() {
  const [health, setHealth] = useState<HealthResponse | null>(null);
  const [healthError, setHealthError] = useState<string | null>(null);
  const [items, setItems] = useState<ExampleItem[]>([]);
  const [loading, setLoading] = useState(true);
  const [listError, setListError] = useState<string | null>(null);

  const loadExamples = useCallback(async () => {
    setLoading(true);
    setListError(null);
    try {
      const response = await fetchExamples();
      setItems(response.items);
    } catch (err: unknown) {
      setListError(err instanceof Error ? err.message : 'Unknown error');
    } finally {
      setLoading(false);
    }
  }, []);

  useEffect(() => {
    apiFetch<HealthResponse>('/v1/health')
      .then(setHealth)
      .catch((err: unknown) => {
        setHealthError(err instanceof Error ? err.message : 'Unknown error');
      });
  }, []);

  useEffect(() => {
    let active = true;
    fetchExamples()
      .then((response) => {
        if (active) {
          setItems(response.items);
        }
      })
      .catch((err: unknown) => {
        if (active) {
          setListError(err instanceof Error ? err.message : 'Unknown error');
        }
      })
      .finally(() => {
        if (active) {
          setLoading(false);
        }
      });
    return () => {
      active = false;
    };
  }, []);

  const handleCreate = async (name: string) => {
    await createExample(name);
    await loadExamples();
  };

  return (
    <div>
      <Typography.Title level={3}>Sample Project</Typography.Title>
      <Typography.Paragraph>
        Эталонная фича: список и создание примеров через REST API и FSD-слои.
      </Typography.Paragraph>
      {health && (
        <Typography.Text type="success">
          Backend: {health.service} — {health.status}
        </Typography.Text>
      )}
      {healthError && (
        <Typography.Paragraph type="danger">Backend недоступен: {healthError}</Typography.Paragraph>
      )}
      <Typography.Title level={5} style={{ marginTop: 24 }}>
        Примеры
      </Typography.Title>
      <CreateExampleForm onSubmit={handleCreate} />
      {listError && (
        <Typography.Paragraph type="danger" style={{ marginTop: 16 }}>
          Ошибка загрузки: {listError}
        </Typography.Paragraph>
      )}
      <div style={{ marginTop: 16 }}>
        <ExampleTable items={items} loading={loading} />
      </div>
    </div>
  );
}
