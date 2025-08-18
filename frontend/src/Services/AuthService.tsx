// api.ts
import axios from "axios";
import { handleError } from "../helpers/ErrorHandler";
import type { UserProfileToken } from "../models/User";

// Axios instance
const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_URL || "https://localhost:7165/api",
});


export const loginAPI = async (userName: string, password: string) => {
  try {
    const response = await apiClient.post<UserProfileToken>("/account/login", {
      userName,  
      password,  
    });
    return response;
  } catch (error) {
    handleError(error);
  }
};

// ==============================
// ====== REGISTER API ==========
// ==============================
export const registerAPI = async (
  email: string,
  userName: string,
  password: string
) => {
  try {
    const response = await apiClient.post<UserProfileToken>(
      "/account/register",
      {
        email,    
        userName, 
        password, 
      }
    );
    return response;
  } catch (error) {
    handleError(error);
  }
};
