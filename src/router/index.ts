import { defineRouter } from '#q-app/wrappers';
import {
  createMemoryHistory,
  createRouter,
  createWebHashHistory,
  createWebHistory,
} from 'vue-router';
import routes from './routes';

// Mock authentication function (replace with actual implementation)
const isAuthenticated = (): boolean => !!localStorage.getItem('authToken'); // Check if token exists in local storage

export default defineRouter(function (/* { store, ssrContext } */) {
  const createHistory = process.env.SERVER
    ? createMemoryHistory
    : (process.env.VUE_ROUTER_MODE === 'history' ? createWebHistory : createWebHashHistory);

  const Router = createRouter({
    scrollBehavior: () => ({ left: 0, top: 0 }),
    routes,

    // Configurations related to history mode
    history: createHistory(process.env.VUE_ROUTER_BASE),
  });

  // Global Navigation Guard
  Router.beforeEach((to, from, next) => {
    if (to.meta.requiresAuth && !isAuthenticated()) {
      // Redirect unauthenticated users to the login page
      next('/login');
    } else {
      // Allow navigation to the target route
      next();
    }
  });

  return Router;
});
