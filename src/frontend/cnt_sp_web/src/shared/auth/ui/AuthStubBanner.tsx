import { Alert } from 'antd';
import { isAuthEnabled } from '@/shared/auth/token';

/**
 * Заглушка auth: напоминание о Bearer-токене при `VITE_AUTH_ENABLED=true`.
 * Замените на OIDC/login flow в форке проекта.
 */
export function AuthStubBanner() {
  if (!isAuthEnabled()) {
    return null;
  }

  return (
    <Alert
      type="info"
      showIcon
      message="Auth включён (шаблон)"
      description="POST /examples требует Authorization: Bearer. Dev-токен подставляется автоматически из shared/auth/token.ts."
      style={{ marginBottom: 16 }}
    />
  );
}
