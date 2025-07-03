function getEnv(key: keyof ImportMetaEnv, fallback: string): string {
  const value = import.meta.env[key]
  return value && value.trim() !== '' ? value : fallback
}

export const configs = {
  apiUrl: getEnv('VITE_API_URL', 'localhost'),
  apiPort: getEnv('VITE_API_PORT', '5041'),
}
