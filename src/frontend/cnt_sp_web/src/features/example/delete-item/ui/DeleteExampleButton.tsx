import { App, Button, Popconfirm } from 'antd';
import type { ExampleItem } from '@/entities/example/model/types';

type DeleteExampleButtonProps = {
  item: ExampleItem;
  onDelete: (id: string) => Promise<void>;
};

/**
 * Кнопка удаления примера с подтверждением.
 */
export function DeleteExampleButton({ item, onDelete }: DeleteExampleButtonProps) {
  const { message } = App.useApp();

  return (
    <Popconfirm
      title="Удалить пример?"
      description={`«${item.name}» будет удалён без возможности восстановления.`}
      okText="Удалить"
      cancelText="Отмена"
      onConfirm={async () => {
        try {
          await onDelete(item.id);
          message.success('Пример удалён');
        } catch {
          message.error('Не удалось удалить пример');
        }
      }}
    >
      <Button type="link" danger>
        Удалить
      </Button>
    </Popconfirm>
  );
}
