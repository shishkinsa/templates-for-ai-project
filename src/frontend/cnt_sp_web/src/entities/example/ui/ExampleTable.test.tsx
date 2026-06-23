import { render, screen } from '@testing-library/react';
import { describe, expect, it } from 'vitest';
import { ExampleTable } from '@/entities/example/ui/ExampleTable';

describe('ExampleTable', () => {
  it('renders example item names', () => {
    render(
      <ExampleTable
        items={[
          {
            id: 'a0000001-0000-0000-0000-000000000001',
            name: 'Test item',
            createdAt: '2026-06-23T10:00:00Z',
          },
        ]}
      />,
    );

    expect(screen.getByText('Test item')).toBeInTheDocument();
    expect(screen.getByText('Название')).toBeInTheDocument();
  });
});
