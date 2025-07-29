export interface ProductUpdate {
    id: number;
    name: string;
    description?: string;
    imageUrl: string;
    supplierPrice: number;
    stock: number;
    slug: string;
    categoryId: number;
    brandId: number;
}