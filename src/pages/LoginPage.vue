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

      <div class="column" style="height: 150px">
        <!-- Username Field -->
        <div class="col red-hat-normal-font">
          <div class="login-username-label red-hat-normal-font">Username or Email</div>
          <div class="q-gutter-md" style="width: 350px;">
            <q-input v-model="username" rounded outlined bg-color="white" :error="!!usernameError"
              :error-message="usernameError" />
          </div>
        </div>
        <br>
        <br>
        <br>
        <!-- Password Field -->
        <div class="col red-hat-normal-font">
          <div class="red-hat-normal-font">Password</div>
          <div class="q-gutter-md" style="top: 30px width: 350px">
            <q-input v-model="password" type="password" rounded outlined bg-color="white" :error="!!passwordError"
              :error-message="passwordError" />
          </div>
        </div>
      </div>
      <br>

      <!-- Forget Password and Signup Options -->
      <div class="column items-center" style="height: 180px;">
        <br>
        <div class="login-header-forget-label red-hat-normal-font">
          <div>
            <br>
            <div @click="goToSignUp">If you don’t have any account. Click here.</div>
          </div>
          <br>
          <div @click="goToForgetPassword">Forget password?</div>
        </div>
        <br>
        <!-- Sign In Button -->
        <div class="flex text-center">
          <q-btn class="login-header-button-sign-in red-hat-normal-font" no-caps push color="green" label="Sign in"
            style="width: 100px;" @click="handleLogin" />
        </div>
      </div>
    </div>
  </q-page>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import '../css/login.css';

const username = ref('');
const password = ref('');
const usernameError = ref('');
const passwordError = ref('');
const router = useRouter();

const goToSignUp = async () => {
  try {
    await router.push('/register'); // Replace '/sign-up' with your actual sign-up route
  } catch (error) {
    console.error('Navigation error:', error);
  }
};

const goToForgetPassword = async () => {
  try {
    await router.push('/forget_password'); // Replace '/sign-up' with your actual sign-up route
  } catch (error) {
    console.error('Navigation error:', error);
  }
}

const handleLogin = async () => {
  let valid = true;

  // Validate username
  if (!username.value.trim()) {
    usernameError.value = 'Username or Email is required.';
    valid = false;
  } else if (username.value.length < 3) {
    usernameError.value = 'Username must be at least 3 characters long.';
    valid = false;
  } else {
    usernameError.value = '';
  }

  // Validate password
  if (!password.value.trim()) {
    passwordError.value = 'Password is required.';
    valid = false;
  } else if (password.value.length < 6) {
    passwordError.value = 'Password must be at least 6 characters long.';
    valid = false;
  } else {
    passwordError.value = '';
  }

  // If valid, navigate to another page
  if (valid) {
    try {
      await router.push('/dashboard'); // Replace '/dashboard' with your actual route
    } catch (error) {
      console.error('Navigation error:', error);
    }
  }
};
</script>

