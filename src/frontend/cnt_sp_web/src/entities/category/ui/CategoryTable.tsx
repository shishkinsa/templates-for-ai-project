import { Table } from 'antd';
import type { ColumnsType } from 'antd/es/table';
import type { Category } from '@/entities/category/model/types';

type CategoryTableProps = {
  items: Category[];
  loading?: boolean;
};

/**
 * Таблица справочных категорий (read-only).
 */
export function CategoryTable({ items, loading }: CategoryTableProps) {
  const columns: ColumnsType<Category> = [
    { title: 'Код', dataIndex: 'code', key: 'code' },
    { title: 'Название', dataIndex: 'name', key: 'name' },
  ];

  return (
    <Table
      rowKey="id"
      loading={loading}
      dataSource={items}
      pagination={false}
      columns={columns}
      size="small"
    />
  );
}
