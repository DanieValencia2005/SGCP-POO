// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    const toggleButton = document.getElementById("toggleTheme");
    const fontSizeSelect = document.getElementById("fontSizeSelect");
    const body = document.body;

    // Cargar preferencias guardadas
    const savedTheme = localStorage.getItem("theme");
    const savedFontSize = localStorage.getItem("fontSize") || "medium";

    // Aplicar tema oscuro si estaba guardado
    if (savedTheme === "dark") {
        body.classList.add("dark-mode");
        toggleButton.innerText = "☀️ Tema Claro";
    }

    // Aplicar tamaño de fuente
    body.classList.remove("font-small", "font-medium", "font-large");
    body.classList.add("font-" + savedFontSize);
    fontSizeSelect.value = savedFontSize;

    // Cambiar tema al hacer clic
    toggleButton.addEventListener("click", () => {
        body.classList.toggle("dark-mode");
        const isDark = body.classList.contains("dark-mode");
        toggleButton.innerText = isDark ? "☀️ Tema Claro" : "🌙 Tema Oscuro";
        localStorage.setItem("theme", isDark ? "dark" : "light");
    });

    // Cambiar tamaño de fuente
    fontSizeSelect.addEventListener("change", () => {
        const selectedSize = fontSizeSelect.value;
        body.classList.remove("font-small", "font-medium", "font-large");
        body.classList.add("font-" + selectedSize);
        localStorage.setItem("fontSize", selectedSize);
    });
});
