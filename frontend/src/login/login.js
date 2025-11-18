const form = document.getElementById('loginForm');
const statusEl = document.getElementById('status');

form.addEventListener('submit', async (event) => {
    event.preventDefault();

    const login = form.login.value.trim();
    const password = form.password.value;

    statusEl.textContent = "";
    statusEl.className = "status";

    if (!login || !password) {
        statusEl.textContent = "Uzupełnij wszystkie pola.";
        statusEl.classList.add("error");
        return;
    }

    try {
        statusEl.textContent = "Logowanie...";
        const response = await loginUser(login, password);

        if (!response.ok) {
            statusEl.textContent = response.message || "Nieprawidłowy login lub hasło.";
            statusEl.classList.add("error");
        } else {
            statusEl.textContent = "Zalogowano pomyślnie 🎉";
            statusEl.classList.add("success");

        }
    } catch (e) {
        console.error(e);
        statusEl.textContent = "Wystąpił błąd podczas logowania.";
        statusEl.classList.add("error");
    }
});

async function loginUser(login, password) {
    
    if (login === "admin" && password === "test123") {
        return {
            ok: true,
            token: "fake-jwt-token"
        };
    } else {
        return {
            ok: false,
            message: "Nieprawidłowy login lub hasło."
        };
    }

}
