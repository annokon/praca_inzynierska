document.addEventListener('DOMContentLoaded', () => {
    const form = document.getElementById('loginForm');
    const statusEl = document.getElementById('status');

    if (!form || !statusEl) {
        console.error("Nie znaleziono elementów #loginForm albo #status. Sprawdź id w HTML.");
        return;
    }

    form.addEventListener('submit', async (event) => {
        event.preventDefault();
        console.log("Submit formularza logowania");

        const email = form.email.value.trim();
        const password = form.password.value;

        statusEl.textContent = "";
        statusEl.className = "status";

        if (!email || !password) {
            statusEl.textContent = "Uzupełnij wszystkie pola.";
            statusEl.classList.add("error");
            return;
        }

        try {
            statusEl.textContent = "Logowanie...";
            console.log("Wywołuję loginUser");

            const result = await loginUser(email, password);
            console.log("Wynik loginUser:", result);

            if (!result.ok) {
                statusEl.textContent = result.message || "Nieprawidłowy email lub hasło.";
                statusEl.classList.add("error");
            } else {
                statusEl.textContent = "Zalogowano pomyślnie";
                statusEl.classList.add("success");

                setTimeout(() => {
                    window.location.href = "../index.html";
                }, 1000);
            }
        } catch (e) {
            console.error("Błąd w trakcie logowania:", e);
            statusEl.textContent = "Wystąpił błąd podczas logowania.";
            statusEl.classList.add("error");
        }
    });
});

async function loginUser(email, password) {
    const res = await fetch("http://localhost:5292/api/auth/login", {
        method: "POST",
        credentials: "include",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ email, password })
    });

    if (!res.ok) {
        let message = "Nieprawidłowy email lub hasło.";
        try {
            const data = await res.json();
            if (data.message) message = data.message;
        } catch (_) {}
        return { ok: false, message };
    }

    const data = await res.json().catch(() => ({}));
    return {
        ok: true,
        role: data.role || null,
        message: data.message || "Logged in"
    };
}
