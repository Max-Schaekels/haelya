export interface ProductQuery {
  page: number;
  pageSize: number;
  categoryId?: number;
  brandId?: number;
  minPrice?: number;
  maxPrice?: number;
  search?: string;
  sortBy?: string;
  sortDirection?: 'asc' | 'desc';
  isActive?: boolean; // seulement si admin
}