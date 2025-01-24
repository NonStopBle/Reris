import type { RouteRecordRaw } from 'vue-router';

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: () => import('layouts/MainLayout.vue'),
    children: [{ path: '', component: () => import('pages/IndexPage.vue') }],
  },
  {
    path: '/login',
    component: () => import('layouts/LoginLayout.vue'),
    children: [{ path: '', component: () => import('pages/LoginPage.vue') }],
  },
  {
    path: '/register',
    component: () => import('layouts/LoginLayout.vue'),
    children: [{ path: '', component: () => import('pages/RegisterPage.vue') }],
  },
  {
    path: '/forget_password',
    component: () => import('layouts/ForgetPasswordLayout.vue'),
    children: [{ path: '', component: () => import('pages/ForgetPasswordPage.vue') }],
  },

  {
    path: '/register_verify',
    component: () => import('layouts/LoginLayout.vue'),
    children: [{ path: '', component: () => import('pages/RegisterVerifyPage.vue') }],
  },

  
  // Always leave this as last one,
  // but you can also remove it
  {
    path: '/:catchAll(.*)*',
    component: () => import('pages/ErrorNotFound.vue'),
  },
];

export default routes;
