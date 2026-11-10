export interface CreateCustomerDto {
  masothue: string;
  customerName: string;
  industry: string;
  contactName: string;
  contactNumber: string;
  contactAddress: string;
  contactEmail: string;
  contactAddess: string;
  description: string;
}

export interface CustomerResponseDto extends CreateCustomerDto {
  id: string;
  createdDate: string;
}