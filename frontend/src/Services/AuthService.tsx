// api.ts
import axios from "axios";
import { handleError } from "../Helpers/ErrorHandler";
import type { UserProfileToken } from "../Models/User";

;

// Axios instance
const apiClient = axios.create({
  baseURL: import.meta.env.VITE_API_URL || "http://localhost:7165/api",
});


export const loginAPI = async (username: string, password: string) => {
  try {
    const data = await apiClient.post<UserProfileToken>("/account/login", {
      username,
      password,
    });
    return data; 
  } catch (error) {
    handleError(error);
    
  }
};


export const registerAPI = async (
  email: string,
  username: string,
  password: string
) => {
  try {
    const data = await apiClient.post<UserProfileToken>(
      "/account/register",
      { email, username, password }
    );
    return data;
  } catch (error) {
    handleError(error);
    
  }
};
