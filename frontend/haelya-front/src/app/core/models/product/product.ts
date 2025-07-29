export interface Product {
  id: number;
  name: string;
  description?: string;
  imageUrl: string;
  price: number;
  supplierPrice: number;
  margin: number;
  stock: number;
  slug: string;
  categoryId: number;
  categoryName: string;
  brandId: number;
  brandName: string;
  viewCount: number;
  avgNote: number;
  inSlide: boolean;
  isActive: boolean;
  featured: boolean;
}