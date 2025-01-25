<template>
  <q-layout view="lHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-btn flat dense round icon="menu" aria-label="Menu" @click="toggleLeftDrawer" />

        <q-toolbar-title>
          Quasar App
        </q-toolbar-title>

        <!-- Display account name if available -->
        <div v-if="username_readable" class="q-ml-auto">
          <span>Welcome, {{ username_readable }} </span>
        </div>

        <!-- Logout Button -->
        <div style="margin-left: 20px;">
          <q-btn flat dense label="Logout" color="negative" @click="logout" />
        </div>
      </q-toolbar>
    </q-header>

    <q-drawer v-model="leftDrawerOpen" show-if-above bordered>
      <q-list>
        <q-item-label header>
          Essential Links
        </q-item-label>

        <EssentialLink v-for="link in linksList" :key="link.title" v-bind="link" />

        <!-- Logout Button in Drawer (optional) -->
        <q-item clickable @click="logout">
          <q-item-section>
            <q-item-label>Logout</q-item-label>
          </q-item-section>
        </q-item>
      </q-list>
    </q-drawer>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue';
import axios from 'axios';
let username_readable = "";
// Initialize links list (optional, you can modify as needed)
const linksList = [
  { title: 'Docs', caption: 'quasar.dev', icon: 'school', link: 'https://quasar.dev' },
  { title: 'Github', caption: 'github.com/quasarframework', icon: 'code', link: 'https://github.com/quasarframework' },
  { title: 'Discord Chat Channel', caption: 'chat.quasar.dev', icon: 'chat', link: 'https://chat.quasar.dev' },
  { title: 'Forum', caption: 'forum.quasar.dev', icon: 'record_voice_over', link: 'https://forum.quasar.dev' },
  { title: 'Twitter', caption: '@quasarframework', icon: 'rss_feed', link: 'https://twitter.quasar.dev' },
  { title: 'Facebook', caption: '@QuasarFramework', icon: 'public', link: 'https://facebook.quasar.dev' },
  { title: 'Quasar Awesome', caption: 'Community Quasar projects', icon: 'favorite', link: 'https://awesome.quasar.dev' }
];

const leftDrawerOpen = ref(false);

// Toggle the left drawer visibility
function toggleLeftDrawer() {
  leftDrawerOpen.value = !leftDrawerOpen.value;
}

// Logout function to clear localStorage and redirect to login
function logout() {
  localStorage.clear();
  // Optionally, redirect to the login page or home page
  // This assumes you have routing set up for login or home page
  window.location.href = '/'; // Or use `router.push('/login')` if using Vue Router
}

// On mounted, fetch the username from localStorage and make the profile API call
onMounted(async () => {
  const token = localStorage.getItem('authToken'); // Get token from localStorage
  
  if (!token) {
    alert('User not found!');
    return;
  }



  try {
  const response = await axios.post(
    'http://localhost:5184/api/auth/profile', // API URL
    {}, // Request body can be empty if you're only sending the token for authentication
    {
      headers: {
        Authorization: `Bearer ${token}` // Pass token as Bearer token in the header
      }
    }
  );

  console.log(response);
  if (response && response.data && response.data.username) {
    // Handle successful response if necessary
    console.log('Profile response:', response.data);
    username_readable = response.data.username;
    localStorage.setItem('username', username_readable); // Store username in localStorage
  }
} catch (error) {
  console.error('Error during the request:', error);
}

});
</script>

<style scoped>
/* Add your custom styles if needed */
</style>
