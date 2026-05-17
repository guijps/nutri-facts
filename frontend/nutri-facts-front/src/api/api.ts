const API_URL = "http://localhost:5294";

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