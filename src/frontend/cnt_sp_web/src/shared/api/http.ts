/**
 * Базовый HTTP-клиент для вызовов `/api`.
 *
 * @param path — путь относительно `/api`
 * @param init — параметры fetch
 * @returns распарсенный JSON
 */
export async function apiFetch<T>(path: string, init?: RequestInit): Promise<T> {
  const response = await fetch(`/api${path}`, {
    headers: { Accept: 'application/json', ...init?.headers },
    ...init,
  });

  if (!response.ok) {
    throw new Error(`API error: ${response.status}`);
  }

  return response.json() as Promise<T>;
}
