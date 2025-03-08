export interface ProductDto {
    id: number,
    name: string,
    slug?: string,
    description?: string,
    price: number,
    createdAt?: Date,
    updatedAt?: Date,
    isDeleted?: boolean,
}