import { AxiosResponse } from "axios";
import { OrderRequestDto } from "../../models/dtos/Order/OrderRequestDto";
import apiClient from "./apiClient";

const orderApi = {
    checkOut: (orderRequestDto: OrderRequestDto): Promise<AxiosResponse<OrderRequestDto>> => {
        return apiClient.post("/order/check-out", orderRequestDto);
    },
    cancel: (id: number): Promise<AxiosResponse> => {
        return apiClient.put(`/order/cancel/${id}`);
    }
}

export default orderApi;