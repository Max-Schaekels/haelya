export interface ProductCreate {
    name: string;
    description?: string;
    imageUrl: string;
    supplierPrice: number;
    margin: number;
    stock: number;
    categoryId: number;
    brandId: number;
}