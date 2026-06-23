import { App, Form, Input, Modal } from 'antd';
import { useEffect } from 'react';
import type { ExampleItem } from '@/entities/example/model/types';

type EditExampleModalProps = {
  item: ExampleItem | null;
  open: boolean;
  onClose: () => void;
  onSubmit: (id: string, name: string) => Promise<void>;
};

/**
 * Модальное окно редактирования примера сущности.
 */
export function EditExampleModal({ item, open, onClose, onSubmit }: EditExampleModalProps) {
  const { message } = App.useApp();
  const [form] = Form.useForm<{ name: string }>();

  useEffect(() => {
    if (open && item) {
      form.setFieldsValue({ name: item.name });
    }
  }, [form, item, open]);

  return (
    <Modal
      title="Редактировать пример"
      open={open}
      onCancel={onClose}
      onOk={() => form.submit()}
      destroyOnHidden
    >
      <Form
        form={form}
        layout="vertical"
        onFinish={async (values) => {
          if (!item) {
            return;
          }

          try {
            await onSubmit(item.id, values.name);
            message.success('Пример обновлён');
            onClose();
          } catch {
            message.error('Не удалось обновить пример');
          }
        }}
      >
        <Form.Item
          name="name"
          label="Название"
          rules={[{ required: true, message: 'Введите название' }, { max: 256 }]}
        >
          <Input />
        </Form.Item>
      </Form>
    </Modal>
  );
}
