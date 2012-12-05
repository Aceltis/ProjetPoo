#ifdef WANTDLLEXP
	#define DLL _declspec(dllexport)
	#define EXTERNC extern "C"
#else
	#define DLL
	#define EXTERNC
#endif

class DLL Algo {
	public:
		Algo() {}
		~Algo() {}
		int computeFoo();
};

EXTERNC DLL Algo* Algo_new();
EXTERNC DLL void Algo_delete(Algo* algo);
EXTERNC DLL int Algo_computeAlgo(Algo* algo);