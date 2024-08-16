/*
 * Created by SharpDevelop.
 * User: Роман
 * Date: 18.06.2023
 * Time: 0:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace statistic
{
	class Program
	{
		public static void Main(string[] args)
		{
			double g,al,a,b;
			string s;
			Console.WriteLine("Надёжность?");
			s=Console.ReadLine();
			g=double.Parse(s);
			Console.WriteLine("значимость?");
			s=Console.ReadLine();
			al=double.Parse(s);
			Console.WriteLine("Начало?");
			s=Console.ReadLine();
			a=double.Parse(s);
			Console.WriteLine("Кончало?");
			s=Console.ReadLine();
			b=double.Parse(s);
			
			double [] m = new double [200];//data
			Console.WriteLine("Давай циферки");
			for (int i=0;i<200;i++) 
			{
				Console.Write("{0}\t",i);
				s=Console.ReadLine();
				m[i]=double.Parse(s);
			}
						
			Console.ReadKey(true);
			Console.ReadKey(true);
			Console.ReadKey(true);
			
			double h=(b-a)/10;
			double [] res = new double [10];//result
			for (int i=0;i<10;i++)res[i]=0;
			foreach (double i in m)
			{
				if(i<=a+h)res[0]++;
				else if(i<=a+2*h&&i>a+h)res[1]++;
				else if(i<=a+3*h&&i>a+2*h)res[2]++;
				else if(i<=a+4*h&&i>a+3*h)res[3]++;
				else if(i<=a+5*h&&i>a+4*h)res[4]++;
				else if(i<=a+6*h&&i>a+5*h)res[5]++;
				else if(i<=a+7*h&&i>a+6*h)res[6]++;
				else if(i<=a+8*h&&i>a+7*h)res[7]++;
				else if(i<=a+9*h&&i>a+8*h)res[8]++;
				else if(i<=a+10*h&&i>a+9*h)res[9]++;
			}
			
			Console.WriteLine("а)Интервальный статистический ряд");
			Console.WriteLine("");
			Console.WriteLine("Частоты ni");
			for (int i=0;i<10;i++) Console.Write("{0}\t",res[i]);
			Console.WriteLine("\nПлотность частот ni/h");
			for (int i=0;i<10;i++) Console.Write("{0}\t",res[i]/h);
			Console.WriteLine("\nОтносительные частоты wi=ni/n");
			for (int i=0;i<10;i++) Console.Write("{0}\t",res[i]/200.0);
			Console.WriteLine("\nПлотность относительных частот wi/h");
			for (int i=0;i<10;i++) Console.Write("{0}\t",res[i]/(h*200.0));
			
			Console.WriteLine("\nб)График относительных частот строй по последней строке сверху, это результаты от {0} с шагом {1}",a,h);
			
			double x=0;
			for (int i=0;i<200;i++)x+=m[i]/200.0;
			Console.WriteLine("в.1) точечная оценка математического ожидания - это выборочное среднее, т.е. xср = сумма xi/n= {0}, без ni, так как значения не повторяются",x);
			
			double d=0;
			for (int i=0;i<200;i++)
			{
				d+=(m[i]-x)*(m[i]-x)/200.0;
			}
			Console.WriteLine("в.2) дисперсия d= сумма (xi-хср)^2/n {0}",d);
			
			Console.WriteLine("Чтобы найти интервальную оценку математического ожидания хср-(t[g]*S/sqrt(n))<mo<хср+(t[g]*S/sqrt(n))\nнужна сама эта t[g], она в табличке Стьюдента смотрится по гамма={0} и n=200, укажи её. Ну или потаблице значений функии Лапласса, где Ф(t[g])=gamma/2={1}. http://mathprofi.ru/files/u/laplas.png ",g,g/2.0);
			s=Console.ReadLine();
			double gg=double.Parse(s);
			double sum=Math.Sqrt(200.0*d/199.0);//исправленное среднее квадратичное отклонение
			Console.WriteLine("в.3) исправленное среднее квадратичное отклонение s=sqrt(s*s)=sqrt(n*d/n-1)={0}",sum);
			
			Console.WriteLine("в.4) интервальная оценка МО=целое число между {0} и {1}",x+gg*sum/Math.Sqrt(200),x-gg*sum/Math.Sqrt(200));
			
			Console.WriteLine("г) Гипотеза H0: распределение генеральной совокупности подчинено нормальному закону распределения с параметрами xcp={0} и s={1}",x,sum);
			Console.WriteLine("Рассчитаем теоретические частоты n_i^0=n*h*fi(ui)/S, где ui=(xi-xcp)/s, а fi(u)=1/2Pi * e^-(uu/2)");
			double [] no = new double [200];//nio
			double xnabl=0;//Хнабл^2
			for (int i=0;i<200;i++)
			{
				no[i]=200.0*h*Math.Pow(Math.Exp(x), 0.0-( m[i]-x)*(m[i]-x)/(sum*sum*2.0))/(sum*Math.Sqrt(2*Math.PI));
			}
			for (int i=0;i<200;i++)
			{
				xnabl+=(1.0-no[i])*(1.0-no[i])/no[i];
			}
			Console.WriteLine("По ним найдём Хнабл^2=сумма (ni-ni0)/ni0={0}",xnabl);//Вроде числа ни в одной выборне не повторялись, так что частоты у всех 1 считаю
			Console.WriteLine("Хкр^2 смотри по табличке при a={0},k=199. Если Хнабл^2 меньше Хкр^2, то всё норм. https://docs.google.com/spreadsheets/d/1jzyP_slielKpLpPkStWofs27qNxg-HPxZ9iw2M01GI4/edit#gid=1858204701 (11б)",al);
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}