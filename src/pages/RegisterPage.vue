<template>
  <q-page class="row items-center justify-evenly" style="background-color: #f5f5f5;">
    <div>
      <div class="column items-center no-select" style="height: 150px;">
        <div class="flex text-center">
          <div class="login-header-title-label no-warp red-hat-normal-font">RERIS</div>
        </div>
        <div class="flex text-center">
          <div class="login-header-subtitle-label red-hat-normal-font">Robotics Interface System</div>
        </div>
      </div>

      <div class="column" style="height: 150px;">
        <div class="col red-hat-normal-font">
          <div class="login-username-label red-hat-normal-font">Username or Email</div>
          <div class="q-gutter-md" style="width: 350px;">
            <q-input v-model="username" rounded outlined bg-color="white" />
          </div>
        </div>
        <br><br>
        <div class="col red-hat-normal-font">
          <div class="red-hat-normal-font">Password</div>
          <div class="q-gutter-md" style="width: 350px;">
            <q-input v-model="password" type="password" rounded outlined bg-color="white" />
          </div>
        </div>
      </div>

      <br><br>
      <div class="column" style="height: 150px;">
        <div class="col red-hat-normal-font">
          <div class="login-username-label red-hat-normal-font">Confirm Password</div>
          <div class="q-gutter-md" style="width: 350px;">
            <q-input v-model="confirmPassword" type="password" rounded outlined bg-color="white" />
          </div>
        </div>
        <br><br>
        <div class="col red-hat-normal-font">
          <div class="red-hat-normal-font">Role</div>
          <div class="q-gutter-md" style="width: 350px;">
            <q-select v-model="role" rounded outlined :options="options" label="Choose an option" />
          </div>
        </div>
      </div>

      <br><br>
      <div class="column items-center" style="height: 150px;">
        <div class="login-header-forget-label red-hat-normal-font">
          <div @click="goToSignIn">If you already have an account, click here.</div>
          <br>
          <div @click="goToForgetPassword">Forgot password?</div>
        </div>
        <br>
        <div class="flex text-center">
          <q-btn class="login-header-button-sign-in red-hat-normal-font" no-caps push color="" label="Sign up"
            style="width: 100px; background-color: #4BCC53;" @click="registerUser" />
        </div>
      </div>
    </div>
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';

const options = ref(['Member']);
const router = useRouter();

// Reactive form data
const username = ref('');
const password = ref('');
const confirmPassword = ref('');
const role = ref('Member');

// Go to sign-in page
const goToSignIn = async () => {
  try {
    await router.push('/login');
  } catch (error) {
    console.error('Navigation error:', error);
  }
};

// Go to forget password page
const goToForgetPassword = async () => {
  try {
    await router.push('/forget_password');
  } catch (error) {
    console.error('Navigation error:', error);
  }
};

// Register user and go to the verify page
const registerUser = async () => {
  if (password.value !== confirmPassword.value) {
    alert('Passwords do not match!');
    return;
  }

  try {
    // API call to register the user
    const response = await axios.post('http://localhost:5184/api/auth/register', {
      username: username.value,
      password: password.value,
      role: role.value,
    });

    // Store the token in localStorage
    const token = response.data.token;
    localStorage.setItem('username' , username.value);
    
    localStorage.setItem('authToken', token);

    console.log('User registered successfully:', response.data.message);

    // Navigate to the register verify page
    await router.push('/register_verify');
  } catch (error) {
    console.error('Registration error:', error);
    alert('Registration failed! Please try again.');
  }
};
</script>
