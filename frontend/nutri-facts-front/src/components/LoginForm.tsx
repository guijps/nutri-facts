import { useNavigate } from "react-router-dom";
export function LoginForm() {
  const navigate = useNavigate();

  async function handleLogin(
    event: React.FormEvent<HTMLFormElement>
  ) {
    event.preventDefault();

    const formData = new FormData(event.currentTarget);

    const email = formData.get("email");
    const password = formData.get("password");

    try {
      const response = await fetch(
        "http://localhost:5294/api/auth/login",
        {
          method: "POST",

          headers: {
            "email": email as string,
            "password": password as string,
          },
        }
      );

      if (!response.ok) {
        alert("Invalid credentials");
        return;
      }

      const data = await response.json();

      localStorage.setItem("token", data.token);

      alert("Login successful");

      console.log("JWT:", data.token);
      navigate("/home");
    } catch (error) {
      console.error(error);

      alert("Failed to connect to API");
    }
  }

  return (
    <form onSubmit={handleLogin} className="space-y-5">
      <div>
        <label className="block mb-2 text-sm font-medium">
          Email
        </label>

        <input
          name="email"
          type="email"
          placeholder="testuser@example.com"
          className="w-full border rounded-2xl p-3 outline-none focus:ring-2 focus:ring-black"
          required
        />
      </div>

      <div>
        <label className="block mb-2 text-sm font-medium">
          Password
        </label>

        <input
          name="password"
          type="password"
          placeholder="passwordhash"
          className="w-full border rounded-2xl p-3 outline-none focus:ring-2 focus:ring-black"
          required
        />
      </div>

      <button
        type="submit"
        className="w-full bg-black text-white rounded-2xl p-3 font-semibold hover:opacity-90 transition"
      >
        Login
      </button>
    </form>
  );
}