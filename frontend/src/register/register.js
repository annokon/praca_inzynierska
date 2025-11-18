const form = document.getElementById('registerForm');
const statusEl = document.getElementById('status');

form.addEventListener('submit', async (event) => {
    event.preventDefault();

    const username = form.username.value.trim();
    const displayName = form.displayName.value.trim();
    const email = form.email.value.trim();
    const birthDate = form.birthDate.value;
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
        const response = await registerUser({
            username,
            displayName,
            email,
            birthDate,
            password
        });

        if (!response.ok) {
            statusEl.textContent = response.message || "Nie udało się utworzyć konta.";
            statusEl.classList.add("error");
        } else {
            statusEl.textContent = "Konto zostało utworzone. Możesz się teraz zalogować.";
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
    console.log("Rejestracja (demo):", data);

    return {
        ok: true,
        message: "Utworzono konto (demo)."
    };

}
