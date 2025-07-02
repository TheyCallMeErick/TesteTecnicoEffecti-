export type Paginated<T> = {
  totalItems: number
  page: number
  pageSize: number
  totalPages: number
  data: T[]
}
