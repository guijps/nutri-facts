const API_URL = "https://192.168.1.16:7270";

export async function api(
  endpoint: string,
  options: RequestInit = {}
) {
  const token = localStorage.getItem("token");

  return fetch(`${API_URL}${endpoint}`, {
    ...options,

    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
      ...options.headers
    }
  });
}