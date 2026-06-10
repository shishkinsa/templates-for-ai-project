import { Table } from 'antd';
import type { ExampleItem } from '@/entities/example/model/types';

type ExampleTableProps = {
  items: ExampleItem[];
  loading?: boolean;
};

/**
 * Таблица примеров сущностей.
 */
export function ExampleTable({ items, loading }: ExampleTableProps) {
  return (
    <Table
      rowKey="id"
      loading={loading}
      dataSource={items}
      pagination={false}
      columns={[
        { title: 'Название', dataIndex: 'name', key: 'name' },
        {
          title: 'Создано',
          dataIndex: 'createdAt',
          key: 'createdAt',
          render: (value: string) => new Date(value).toLocaleString(),
        },
      ]}
    />
  );
}
