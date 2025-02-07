import apiClient from "./apiClient";
import { AxiosResponse } from "axios";
import { Color } from "../../models/entities/Color";

const colorApi = {                  
  getAll: async (): Promise<AxiosResponse<Color[]>> => {
    return await apiClient.get("/color");
  },

  getById: (id: number): Promise<AxiosResponse<Color>> => {
    return apiClient.get(`/color/${id}`);
  },

  create: (data: Color): Promise<AxiosResponse<Color>> => {
    return apiClient.post("/color", data);
  },

  update: (id: number, data: Color): Promise<AxiosResponse<Color>> => {
    return apiClient.put(`/color/${id}`, data);
  },

  delete: (id: number): Promise<AxiosResponse<Color>> => {
    return apiClient.delete(`/color/${id}`);
  }
};

export default colorApi;
