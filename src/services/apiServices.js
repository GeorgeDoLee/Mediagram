const BASE_URL = 'https://localhost:7040/api';

const apiRequest = async (endpoint, method = 'GET', data = null, token = null) => {
    const headers = {
        'Content-Type': 'application/json',
    };

    if (token) {
        headers['Authorization'] = `Bearer ${token}`;
    }

    try {
        const response = await fetch(`${BASE_URL}${endpoint}`, {
            method,
            headers,
            body: data ? JSON.stringify(data) : null,
        });

        if (!response.ok) {
            throw new Error(`Error: ${response.statusText}`);
        }

        return await response.json();
    } catch (error) {
        throw error;
    }
};

// Publisher
export const postPublisher = (data, token) => apiRequest('/Publisher', 'POST', data, token);
export const putPublisher = (id, data, token) => apiRequest(`/Publisher/${id}`, 'PUT', data, token);
export const deletePublisher = (id, token) => apiRequest(`/Publisher/${id}`, 'DELETE', null, token);

// Article
export const postArticle = (data, token) => apiRequest('/Article', 'POST', data, token);
export const deleteArticle = (id, token) => apiRequest(`/Article/${id}`, 'DELETE', null, token);

// Category
export const postCategory = (data, token) => apiRequest('/Category', 'POST', { name: data.name }, token);
export const putCategory = (id, data, token) => apiRequest(`/Category/${id}`, 'PUT', { name: data.name }, token);
export const deleteCategory = (id, token) => apiRequest(`/Category/${id}`, 'DELETE', null, token);

// Auth
export const registerAdmin = (data) => apiRequest('/Auth/register', 'POST', data);
export const login = (data) => apiRequest('/Auth/login', 'POST', data);
