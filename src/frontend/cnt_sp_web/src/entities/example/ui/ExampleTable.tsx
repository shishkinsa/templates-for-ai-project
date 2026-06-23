import { Table } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import type { ExampleItem } from '@/entities/example/model/types';
import type { ReactNode } from 'react';

type ExampleTableProps = {
  items: ExampleItem[];
  loading?: boolean;
  renderActions?: (item: ExampleItem) => ReactNode;
};

/**
 * Таблица примеров сущностей.
 */
export function ExampleTable({ items, loading, renderActions }: ExampleTableProps) {
  const columns: ColumnsType<ExampleItem> = [
    { title: 'Название', dataIndex: 'name', key: 'name' },
    {
      title: 'Создано',
      dataIndex: 'createdAt',
      key: 'createdAt',
      render: (value: string) => new Date(value).toLocaleString(),
    },
  ];

  if (renderActions) {
    columns.push({
      title: 'Действия',
      key: 'actions',
      render: (_value, record) => renderActions(record),
    });
  }

  return (
    <Table
      rowKey="id"
      loading={loading}
      dataSource={items}
      pagination={false}
      columns={columns}
    />
  );
}
