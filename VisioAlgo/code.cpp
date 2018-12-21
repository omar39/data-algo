    #include<bits/stdc++.h>
    using namespace std;
    vector<bool> prime;
    vector<int> num;
    void sieve(int n)
    {
     for(int i = 2;i <= n;++i)
    {
        if(!prime[i])
      num.push_back(i);
      for(int j = i;j*i <= n;++j)
        prime[i*j] = 1;

    }
    }

    int main()
    {
    stack<int> sk;
      int n;
    cin >> n;
     while(n>0)
     {
    sk.push(n%2);
    n/=2;
     }

    while(sk.size())
    cout << sk.top(), sk.pop();


    return 0;
    }
