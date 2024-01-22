import '@/app/ui/global.css';
import { inter } from './ui/fonts';

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  const randomQueryParam = Math.random().toString(36).substring(7);

  return (
    <html lang="en">
      <head>
        <link rel="icon" href={`/favicon.ico?${randomQueryParam}`} />
      </head>
      <title>Dashboard-App</title>
      <body className={`${inter.className} antialiased`}>{children}</body>
    </html>
  );
}
