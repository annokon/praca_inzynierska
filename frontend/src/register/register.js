const form = document.getElementById('registerForm');
const statusEl = document.getElementById('status');

form.addEventListener('submit', async (event) => {
    event.preventDefault();

    const username = form.username.value.trim();
    const displayName = form.displayName.value.trim();
    const email = form.email.value.trim();
    const birthDate = form.birthDate.value; // format yyyy-mm-dd
    const password = form.password.value;
    const confirmPassword = form.confirmPassword.value;
    const termsAccepted = form.terms.checked;

    statusEl.textContent = "";
    statusEl.className = "status";

    if (!username || !displayName || !email || !birthDate || !password || !confirmPassword) {
        statusEl.textContent = "Uzupełnij wszystkie pola.";
        statusEl.classList.add("error");
        return;
    }

    if (!validateEmail(email)) {
        statusEl.textContent = "Podaj poprawny adres e-mail.";
        statusEl.classList.add("error");
        return;
    }

    if (password.length < 8) {
        statusEl.textContent = "Hasło musi mieć co najmniej 8 znaków.";
        statusEl.classList.add("error");
        return;
    }

    if (password !== confirmPassword) {
        statusEl.textContent = "Hasła nie są takie same.";
        statusEl.classList.add("error");
        return;
    }

    if (!termsAccepted) {
        statusEl.textContent = "Musisz zaakceptować regulamin i potwierdzić wiek.";
        statusEl.classList.add("error");
        return;
    }

    try {
        statusEl.textContent = "Tworzenie konta...";

        const result = await registerUser({
            username,
            displayName,
            email,
            birthDate,
            password
        });

        if (!result.ok) {
            statusEl.textContent = result.message || "Nie udało się utworzyć konta.";
            statusEl.classList.add("error");
        } else {
            statusEl.textContent = "Konto zostało utworzone. Przekierowuję do logowania...";
            statusEl.classList.add("success");

            setTimeout(() => {
                window.location.href = "../login/login.html";
            }, 1500);
        }
    } catch (e) {
        console.error(e);
        statusEl.textContent = "Wystąpił błąd podczas rejestracji.";
        statusEl.classList.add("error");
    }
});

function validateEmail(email) {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
}

async function registerUser(data) {
    const res = await fetch("http://localhost:5292/api/users", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({
            username: data.username,
            displayName: data.displayName,
            email: data.email,
            birthDate: data.birthDate,
            password: data.password
        })
    });

    if (!res.ok) {
        let message = "Błąd rejestracji.";
        try {
            const err = await res.json();
            if (err.message) message = err.message;
        } catch (_) {}
        return { ok: false, message };
    }

    const created = await res.json().catch(() => ({}));
    return { ok: true, user: created };
}
