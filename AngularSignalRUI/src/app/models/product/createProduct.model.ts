export interface CreateProduct {
  productName: string;
  description: string;
  price: number;
  imageUrl: string;
  productStatus: boolean;
  categoryID: number;
}
