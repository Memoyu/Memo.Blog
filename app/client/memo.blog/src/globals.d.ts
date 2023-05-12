declare module '*.scss';
declare module 'marked';
declare module '*.md' {
  const value: string; // markdown is just a string
  export default value;
}
