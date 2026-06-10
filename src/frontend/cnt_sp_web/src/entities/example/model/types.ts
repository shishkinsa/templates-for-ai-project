/** DTO примера сущности из API. */
export type ExampleItem = {
  id: string;
  name: string;
  createdAt: string;
};

export type ListExamplesResponse = {
  items: ExampleItem[];
};

export type CreateExampleResponse = {
  item: ExampleItem;
};
