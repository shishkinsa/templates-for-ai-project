import { Button, Space, Typography } from 'antd';
import { useCallback, useEffect, useState } from 'react';
import {
  createExample,
  deleteExample,
  ExampleTable,
  fetchExamples,
  updateExample,
  type ExampleItem,
} from '@/entities/example';
import {
  CategoryTable,
  fetchCategories,
  type Category,
} from '@/entities/category';
import { CreateExampleForm } from '@/features/example/create-item';
import { DeleteExampleButton } from '@/features/example/delete-item';
import { EditExampleModal } from '@/features/example/edit-item';
import { apiFetch } from '@/shared/api/http';

type HealthResponse = { status: string; service: string };

/**
 * Стартовая страница: health-check и эталонный CRUD примеров.
 */
export function HomePage() {
  const [health, setHealth] = useState<HealthResponse | null>(null);
  const [healthError, setHealthError] = useState<string | null>(null);
  const [items, setItems] = useState<ExampleItem[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [categoriesLoading, setCategoriesLoading] = useState(true);
  const [listError, setListError] = useState<string | null>(null);
  const [editingItem, setEditingItem] = useState<ExampleItem | null>(null);

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
    fetchCategories()
      .then((response) => {
        if (active) {
          setCategories(response.items);
        }
      })
      .finally(() => {
        if (active) {
          setCategoriesLoading(false);
        }
      });
    return () => {
      active = false;
    };
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

  const handleUpdate = async (id: string, name: string) => {
    await updateExample(id, name);
    await loadExamples();
  };

  const handleDelete = async (id: string) => {
    await deleteExample(id);
    await loadExamples();
  };

  return (
    <div>
      <Typography.Title level={3}>Sample Project</Typography.Title>
      <Typography.Paragraph>
        Эталонная фича: полный CRUD примеров через REST API и FSD-слои (OpenSpec pilot).
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
        Категории (read-only)
      </Typography.Title>
      <CategoryTable items={categories} loading={categoriesLoading} />
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
        <ExampleTable
          items={items}
          loading={loading}
          renderActions={(item) => (
            <Space size="small">
              <Button type="link" onClick={() => setEditingItem(item)}>
                Изменить
              </Button>
              <DeleteExampleButton item={item} onDelete={handleDelete} />
            </Space>
          )}
        />
      </div>
      <EditExampleModal
        item={editingItem}
        open={editingItem !== null}
        onClose={() => setEditingItem(null)}
        onSubmit={handleUpdate}
      />
    </div>
  );
}
