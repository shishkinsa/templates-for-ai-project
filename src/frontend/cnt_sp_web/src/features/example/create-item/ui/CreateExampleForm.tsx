import { App, Button, Form, Input } from 'antd';

type CreateExampleFormProps = {
  onSubmit: (name: string) => Promise<void>;
};

/**
 * Форма создания примера сущности.
 */
export function CreateExampleForm({ onSubmit }: CreateExampleFormProps) {
  const { message } = App.useApp();
  const [form] = Form.useForm<{ name: string }>();

  return (
    <Form
      form={form}
      layout="inline"
      onFinish={async (values) => {
        try {
          await onSubmit(values.name);
          form.resetFields();
          message.success('Пример создан');
        } catch {
          message.error('Не удалось создать пример');
        }
      }}
    >
      <Form.Item
        name="name"
        rules={[{ required: true, message: 'Введите название' }, { max: 256 }]}
      >
        <Input placeholder="Название примера" />
      </Form.Item>
      <Form.Item>
        <Button type="primary" htmlType="submit">
          Создать
        </Button>
      </Form.Item>
    </Form>
  );
}
