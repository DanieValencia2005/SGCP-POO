document.addEventListener("DOMContentLoaded", function () {
    const toggleThemeBtn = document.getElementById("toggleTheme");
    const fontSizeSelect = document.getElementById("fontSizeSelect");
    const togglePositionBtn = document.getElementById("toggleMenuPosition");
    const body = document.body;

    // Recuperar configuraciones previas
    const savedTheme = localStorage.getItem("theme") || "light";
    const savedFontSize = localStorage.getItem("fontSize") || "medium";
    const menuPosition = localStorage.getItem("menuPosition") || "left";

    // Aplicar tema oscuro/claro
    if (savedTheme === "dark") {
        body.classList.add("dark-mode");
        toggleThemeBtn.innerText = "☀️ Tema Claro";
    } else {
        toggleThemeBtn.innerText = "🌙 Tema Oscuro";
    }

    // Aplicar tamaño de fuente
    body.classList.remove("font-small", "font-medium", "font-large");
    body.classList.add("font-" + savedFontSize);
    fontSizeSelect.value = savedFontSize;

    // Aplicar posición del menú
    if (menuPosition === "right") {
        body.classList.add("menu-right");
    }

    // --- Eventos ---

    // Cambiar tema
    toggleThemeBtn.addEventListener("click", () => {
        body.classList.toggle("dark-mode");
        const isDark = body.classList.contains("dark-mode");
        toggleThemeBtn.innerText = isDark ? "☀️ Tema Claro" : "🌙 Tema Oscuro";
        localStorage.setItem("theme", isDark ? "dark" : "light");
    });

    // Cambiar tamaño de letra
    fontSizeSelect.addEventListener("change", () => {
        const selectedSize = fontSizeSelect.value;
        body.classList.remove("font-small", "font-medium", "font-large");
        body.classList.add("font-" + selectedSize);
        localStorage.setItem("fontSize", selectedSize);
    });

    // Cambiar posición del menú izquierda/derecha
    togglePositionBtn.addEventListener("click", () => {
        body.classList.toggle("menu-right");
        const isRight = body.classList.contains("menu-right");
        localStorage.setItem("menuPosition", isRight ? "right" : "left");
    });
});
