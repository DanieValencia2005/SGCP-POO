document.addEventListener("DOMContentLoaded", function () {
    const toggleThemeBtn = document.getElementById("toggleTheme");
    const fontSizeSelect = document.getElementById("fontSizeSelect");
    const togglePositionBtn = document.getElementById("toggleMenuPosition");
    const toggleMenuTypeBtn = document.getElementById("toggleMenuType");
    const sidebar = document.getElementById("sidebar");
    const body = document.body;

    // Recuperar configuraciones previas
    const savedTheme = localStorage.getItem("theme") || "light";
    const savedFontSize = localStorage.getItem("fontSize") || "medium";
    const menuPosition = localStorage.getItem("menuPosition") || "left";
    const menuType = localStorage.getItem("menuType") || "fixed";

    // Aplicar tema oscuro
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

    // Aplicar tipo de menú
    if (menuType === "collapsible") {
        sidebar.classList.add("collapsible", "show-collapsible");
    }

    // Eventos de botones
    toggleThemeBtn.addEventListener("click", () => {
        body.classList.toggle("dark-mode");
        const isDark = body.classList.contains("dark-mode");
        toggleThemeBtn.innerText = isDark ? "☀️ Tema Claro" : "🌙 Tema Oscuro";
        localStorage.setItem("theme", isDark ? "dark" : "light");
    });

    fontSizeSelect.addEventListener("change", () => {
        const selectedSize = fontSizeSelect.value;
        body.classList.remove("font-small", "font-medium", "font-large");
        body.classList.add("font-" + selectedSize);
        localStorage.setItem("fontSize", selectedSize);
    });

    togglePositionBtn.addEventListener("click", () => {
        body.classList.toggle("menu-right");
        const isRight = body.classList.contains("menu-right");
        localStorage.setItem("menuPosition", isRight ? "right" : "left");
    });

    toggleMenuTypeBtn.addEventListener("click", () => {
        const isCollapsible = sidebar.classList.contains("collapsible");
        if (isCollapsible) {
            sidebar.classList.remove("collapsible", "show-collapsible");
            localStorage.setItem("menuType", "fixed");
        } else {
            sidebar.classList.add("collapsible", "show-collapsible");
            localStorage.setItem("menuType", "collapsible");
        }
    });
});
