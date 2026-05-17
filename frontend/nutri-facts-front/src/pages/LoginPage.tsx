import { LoginForm } from "../components/LoginForm";

export default function LoginPage() {
  
  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100 p-4">
      <div className="w-full max-w-md bg-white rounded-3xl shadow-xl p-8">
        <div className="mb-8 text-center">
          <h1 className="text-3xl font-bold">
            Nutrition App
          </h1>

          <p className="text-gray-500 mt-2">
            Login to continue
          </p>
        </div>

        <LoginForm />
      </div>
    </div>
  );
}