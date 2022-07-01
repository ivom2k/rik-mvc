import axios from "axios";

export const httpClient = axios.create({
    baseURL: "https://localhost:7194/",
    headers: {
        "Content-type": "application/json"
    }
});

export default httpClient;