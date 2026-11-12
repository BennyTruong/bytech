(() => {
    const scrollKey = 'bytech:scroll';

    // Switching language reloads the page; land on the same spot as before.
    try {
        const saved = sessionStorage.getItem(scrollKey);
        sessionStorage.removeItem(scrollKey);
        const fraction = saved === null ? 0 : parseFloat(saved);
        if (fraction > 0) {
            const max = document.documentElement.scrollHeight - window.innerHeight;
            window.scrollTo({ top: fraction * max, behavior: 'instant' });
        }
    } catch {
        // Storage can be blocked; the page then simply starts at the top.
    }

    const setMenuOpen = (open) => {
        const header = document.querySelector('[data-site-header]');
        if (!header) return;
        header.toggleAttribute('data-open', open);
        header.querySelector('[data-menu-toggle]')?.setAttribute('aria-expanded', String(open));
    };

    document.addEventListener('click', (event) => {
        const target = event.target instanceof Element ? event.target : null;
        if (!target) return;

        if (target.closest('[data-lang-toggle]')) {
            try {
                const max = document.documentElement.scrollHeight - window.innerHeight;
                sessionStorage.setItem(scrollKey, max > 0 ? String(window.scrollY / max) : '0');
            } catch {
                // See above.
            }
            return;
        }

        if (target.closest('[data-menu-toggle]')) {
            const header = document.querySelector('[data-site-header]');
            setMenuOpen(!header?.hasAttribute('data-open'));
            return;
        }

        // Any other click (a menu link, or outside the menu) closes it.
        setMenuOpen(false);
    });

    document.addEventListener('keydown', (event) => {
        if (event.key === 'Escape') setMenuOpen(false);
    });
})();
