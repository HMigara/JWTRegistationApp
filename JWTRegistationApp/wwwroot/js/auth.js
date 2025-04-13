
document.addEventListener('DOMContentLoaded', function () {
    // Check if token exists in localStorage
    const token = localStorage.getItem('authToken');
    if (token) {
        fetchUserProfile(token);
    }

    // Tab switching functionality
    const tabs = document.querySelectorAll('.tab');
    const forms = document.querySelectorAll('.form');

    tabs.forEach(tab => {
        tab.addEventListener('click', () => {
            const target = tab.getAttribute('data-tab');

            // Reset messages
            document.getElementById('loginMessage').textContent = '';
            document.getElementById('registerMessage').textContent = '';

            // Switch tabs
            tabs.forEach(t => t.classList.remove('active'));
            tab.classList.add('active');

            // Switch forms
            forms.forEach(form => {
                if (form.id === `${target}Form`) {
                    form.classList.add('active');
                } else {
                    form.classList.remove('active');
                }
            });
        });
    });

    // Login form submission
    document.getElementById('loginForm').addEventListener('submit', async function (e) {
        e.preventDefault();

        const username = document.getElementById('loginUsername').value;
        const password = document.getElementById('loginPassword').value;
        const messageElement = document.getElementById('loginMessage');

        try {
            const response = await fetch('/api/auth/login', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ username, password })
            });

            const data = await response.json();

            if (response.ok) {
                // Store token and show dashboard
                localStorage.setItem('authToken', data.token);
                fetchUserProfile(data.token);

                // Reset form
                document.getElementById('loginForm').reset();
                messageElement.textContent = '';
            } else {
                messageElement.textContent = data || 'Login failed. Please try again.';
                messageElement.classList.add('error');
            }
        } catch (error) {
            messageElement.textContent = 'An error occurred. Please try again.';
            messageElement.classList.add('error');
            console.error('Login error:', error);
        }
    });

    // Register form submission
    document.getElementById('registerForm').addEventListener('submit', async function (e) {
        e.preventDefault();

        const username = document.getElementById('registerUsername').value;
        const password = document.getElementById('registerPassword').value;
        const documentFile = document.getElementById('registerDocument').files[0];
        const registrationDate = document.getElementById('registerDate').value;
        const messageElement = document.getElementById('registerMessage');

        if (!document) {
            messageElement.textContent = 'Please select a document to upload.';
            messageElement.classList.add('error');
            return;
        }

        const formData = new FormData();
        formData.append('username', username);
        formData.append('password', password);
        formData.append('document', documentFile);
        formData.append('registrationDate', registrationDate);

        try {
            const response = await fetch('/api/auth/register', {
                method: 'POST',
                body: formData
            });

            const data = await response.text();

            if (response.ok) {
                messageElement.textContent = 'Registration successful! You can now login.';
                messageElement.classList.add('success');
                messageElement.classList.remove('error');

                // Reset form
                document.getElementById('registerForm').reset();

                // Switch to login tab after successful registration
                setTimeout(() => {
                    document.querySelector('[data-tab="login"]').click();
                }, 2000);
            } else {
                messageElement.textContent = data || 'Registration failed. Please try again.';
                messageElement.classList.add('error');
                messageElement.classList.remove('success');
            }
        } catch (error) {
            messageElement.textContent = 'An error occurred. Please try again.';
            messageElement.classList.add('error');
            messageElement.classList.remove('success');
            console.error('Registration error:', error);
        }
    });

    // Logout functionality
    document.getElementById('logoutBtn').addEventListener('click', function () {
        // Clear token and show login form
        localStorage.removeItem('authToken');
        document.querySelector('.auth-wrapper').classList.remove('hidden');
        document.getElementById('dashboard').classList.add('hidden');
    });

    // Fetch user profile function
    async function fetchUserProfile(token) {
        try {
            const response = await fetch('/api/user/profile', {
                headers: {
                    'Authorization': `Bearer ${token}`
                }
            });

            if (response.ok) {
                const data = await response.json();

                // Update dashboard with user data
                document.getElementById('username').textContent = data.username;
                document.getElementById('registrationDate').textContent = new Date(data.registrationDate).toLocaleDateString();
                document.getElementById('documentLink').href = `/uploads/${data.documentPath}`;

                // Show dashboard, hide auth forms
                document.querySelector('.auth-wrapper').classList.add('hidden');
                document.getElementById('dashboard').classList.remove('hidden');
            } else {
                // If token is invalid, clear it and show login form
                localStorage.removeItem('authToken');
            }
        } catch (error) {
            console.error('Profile error:', error);
            localStorage.removeItem('authToken');
        }
    }
});