#include<bits\stdc++.h>
using namespace std;
vector<bool> prime(1e5 + 1, 0);
vector<int>res;
void sieve()
{
   for(int i=2;i<=10000;++i)
   {
       if(!prime[i]){
         res.push_back(i);

       for(int j = i;j*i<=10000;++j)
       {
           prime[j*i] = 1;
       }
       }
   }
}

int main()
{
stack<int>sk;
sk.pop();
return 0;
}
