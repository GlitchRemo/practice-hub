'use client';
import { useFormState } from 'react-dom';
import { Button } from '../ui/button';
import styled from 'styled-components';
import { x } from '@xstyled/styled-components';
import { useMemo } from 'react';

const increment = async (state: number) => state + 1;

const StyledForm = styled.form`
  background-color: #f4f4f4;
  padding: 20px;
  border-radius: 5px;
  width: 20%;
  margin: auto;
  display: flex;
  justify-content: center;
`;

const Page = () => {
  const [state, dispatch] = useFormState(increment, 0);
  const aConstValue = 1;

  const addOne = (aConstValue: number) => {
    console.log('Function is called', aConstValue);
    return aConstValue + 1;
  };

  const cached = useMemo(() => addOne(aConstValue), [aConstValue]);
  // addOne(aConstValue);

  return (
    <StyledForm>
      <x.div margin="auto" width="50" padding="10px">
        {state}
      </x.div>
      <Button formAction={dispatch}>Increment</Button>
    </StyledForm>
  );
};
export default Page;
