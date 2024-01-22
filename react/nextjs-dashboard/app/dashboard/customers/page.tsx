import { fetchFilteredCustomers } from '@/app/lib/data';
import CustomersTable from '@/app/ui/customers/table';
import { lusitana } from '@/app/ui/fonts';
import Search from '@/app/ui/search';

const Page = async ({ searchParams }: { searchParams?: { query: string } }) => {
  console.log(searchParams);
  const query = searchParams?.query || '';
  console.log(query);
  const customers = await fetchFilteredCustomers(query);

  return (
    <div>
      <h1 className={`${lusitana.className} mb-8 text-xl md:text-2xl`}>
        Customers
      </h1>
      <Search placeholder="Search customers..." />
      <CustomersTable customers={customers} />
    </div>
  );
};

export default Page;
