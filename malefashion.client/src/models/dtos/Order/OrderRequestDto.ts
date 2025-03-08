export interface OrderRequestDto {
    firstName: string;
    lastName: string;
    address: string;
    phoneNumber: string;
    email: string;
    note?: string;
    paymentMethod: "COD" | "PayPal";
}