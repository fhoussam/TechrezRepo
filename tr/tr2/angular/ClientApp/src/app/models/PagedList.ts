export interface PagedList<T> {
  totalPages: number;
  source: T[];
}
