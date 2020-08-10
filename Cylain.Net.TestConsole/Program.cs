using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Cylain.Net.TestConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            TestIPV4CidrParser();
            TestIPV6CidrParser();
            TestSubnetIp();

            Console.ReadKey();
        }

        #region [ TEST AREA ]
        static void TestIPV4CidrParser()
        {
            PrintTestHeader("Valid CIDR IPv4 should NOT throw on parse");
            try
            {
                var cidrIpv4 = CidrIpAddress.Parse("255.255.255.255/32");

                PrintPass();
            }
            catch (Exception e)
            {
                PrintFail("Unexpected exception: " + e.Message);
            }

            PrintTestFooter();
        }

        static void TestIPV6CidrParser()
        {
            PrintTestHeader("Valid CIDR IPv6 should NOT throw on parse");
            try
            {
                var cidrIpv4 = CidrIpAddress.Parse("::/128");

                PrintPass();
            }
            catch (Exception e)
            {
                PrintFail("Unexpected exception: " + e.Message);
            }

            PrintTestFooter();
        }

        static void TestSubnetIp()
        {
            PrintTestHeader("Full bit mask should check all IP parts in order to be considered of the same subnet");
            var cidrIpv4Full = CidrIpAddress.Parse("186.177.128.128/32");

            if (cidrIpv4Full.IsIpFromSameSubnet("186.177.128.129")) PrintFail("Ip expected NOT to be on the same subnet");
            else PrintPass();
            PrintTestFooter();
        }

        static void IpAddressTest()
        {
            var emptyIpV6 = IPAddress.Parse("::");

            var ip = IPAddress.Parse("2001:0db8:0000:0042:0000:8a2e:0370:7334");
            var ip2 = IPAddress.Parse("128.192.128.65");

            var cidrIp = CidrIpAddress.Parse("128.192.164.32/17");
            var cidrIp2 = CidrIpAddress.Parse("2001:0db8:0000:0042:0000:8a2e:0370:7334/64");

            var cidrIpAll = CidrIpAddress.Parse("0.0.0.0/0");
            Console.WriteLine(cidrIpAll.IsIpFromSameSubnet(ip2));
            Console.WriteLine(cidrIpAll.IsIpFromSameSubnet(cidrIp));

            var cidrIpSingle = CidrIpAddress.Parse("186.177.128.128/32");
            var ipSingle = IPAddress.Parse("186.177.128.129"); //even the last bit off should be important when checking
            Console.WriteLine(cidrIpSingle.IsIpFromSameSubnet(ipSingle));

            Console.WriteLine("--------");
            Console.WriteLine(ip.ToString());
            Console.WriteLine(ip2);

            Console.WriteLine(cidrIp.ToString());
            Console.WriteLine(cidrIp.ToBinaryString());

            Console.WriteLine(cidrIp2.ToString());

            Console.WriteLine(cidrIp.IsIpFromSameSubnet(ip2));
            Console.WriteLine(cidrIp.IsIpFromSameSubnet(ip));

            var ip3 = IPAddress.Parse("128.129.128.65");
            Console.WriteLine(cidrIp.IsIpFromSameSubnet(ip3));

            var cidrIp3 = CidrIpAddress.Parse("128.192.128.65/20");
            Console.WriteLine(cidrIp3.ToBinaryString());
            Console.WriteLine(cidrIp.IsIpFromSameSubnet(cidrIp3));
        }
        #endregion

        #region [ TEST OUTPUTS ]
        static void PrintTestHeader(string name)
        {
            Console.WriteLine("========================================");
            Console.WriteLine("Test: " + name);
        }

        static void PrintTestFooter()
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("\n");
        }

        static void PrintFail(string message)
        {
            var oldBgColor = Console.BackgroundColor;
            var oldFgColor = Console.ForegroundColor;

            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write("[FAIL]");
            Console.BackgroundColor = oldBgColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(string.Format(" {0}", message));

            Console.ForegroundColor = oldFgColor;
        }

        static void PrintPass()
        {
            var oldFgColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("[passed]");

            Console.ForegroundColor = oldFgColor;
        }
        #endregion
    }
}
