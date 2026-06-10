import { apiFetch } from '@/shared/api/http';
import type { CreateExampleResponse, ListExamplesResponse } from '@/entities/example/model/types';

/**
 * Загружает список примеров сущностей.
 */
export function fetchExamples(): Promise<ListExamplesResponse> {
  return apiFetch<ListExamplesResponse>('/v1/examples');
}

/**
 * Создаёт пример сущности.
 *
 * @param name — наименование
 */
export function createExample(name: string): Promise<CreateExampleResponse> {
  return apiFetch<CreateExampleResponse>('/v1/examples', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ name }),
  });
}
