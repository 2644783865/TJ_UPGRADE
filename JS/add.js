  function String.prototype.Trim()   
   {
         return this.replace(/\s+/g,"");
    }  
function setClf(textno) 
      { 
            var no=textno;
            var table=document.getElementById("tab"); 
            var tr=table.getElementsByTagName("tr"); 
            var pattem=/^\d+(\.\d+)?$/; 
            var sum1=0,sum2=0,sum3=0,sum4=0,sum5=0,sum6=0,sum7=0,sum8=0,sum9=0; 
            switch(no)
            {
                case 0:
                {
                    var a0;
                    var t0;
                    for( i=4;i <tr.length-1;i++) 
                    { 
                       if(pattem.test(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim();
                            t0=parseFloat(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            t0=0; 
                            tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
                        }
                        ////////////////////////////////////////
                        if(pattem.test(tr[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value.Trim();
                            a0=t0*parseFloat(tr[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            a0=0; 
                            tr[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value="0";
                        }
                         sum1+=a0;
                         //////////////////////////////////////////////
                         if(pattem.test(tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value.Trim();
                            a0=t0*parseFloat(tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            a0=0; 
                            tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value="0";
                        }
                         sum2+=a0;
                         ///////////////////////////////////////////////////
                         if(pattem.test(tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value.Trim();
                            a0=t0*parseFloat(tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            a0=0; 
                            tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value="0";
                        }
                         sum3+=a0;
                         ///////////////////////////////////////////////////////
                         if(pattem.test(tr[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value.Trim();
                            a0=t0*parseFloat(tr[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            a0=0; 
                            tr[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value="0";
                        }
                         sum4+=a0;
                         ////////////////////////////////////////////////////////////////
                         if(pattem.test(tr[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value.Trim();
                            a0=t0*parseFloat(tr[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            a0=0; 
                            tr[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value="0";
                        }
                         sum5+=a0;
                         /////////////////////////////////////
                         if(pattem.test(tr[i].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value.Trim();
                            a0=t0*parseFloat(tr[i].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            a0=0; 
                            tr[i].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value="0";
                        }
                         sum6+=a0;
                         ///////////////////////////////////////
                         if(pattem.test(tr[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value.Trim();
                            a0=t0*parseFloat(tr[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            a0=0; 
                            tr[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value="0";
                        }
                         sum7+=a0;
                         /////////////////////////////////////////////
                         if(pattem.test(tr[i].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value.Trim();
                            a0=t0*parseFloat(tr[i].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            a0=0; 
                            tr[i].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value="0";
                        }
                         sum8+=a0;
                         //////////////////////////////////////////////
                         if(pattem.test(tr[i].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.Trim();
                            a0=t0*parseFloat(tr[i].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            a0=0; 
                            tr[i].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value="0";
                        }
                         sum9+=a0;
                     }
                     tr[tr.length-1].getElementsByTagName("td")[1].getElementsByTagName("input")[0].value=sum1.toFixed(5);
                     tr[tr.length-1].getElementsByTagName("td")[2].getElementsByTagName("input")[0].value=sum2.toFixed(5);
                     tr[tr.length-1].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value=sum3.toFixed(5);
                     tr[tr.length-1].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value=sum4.toFixed(5);
                     tr[tr.length-1].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value=sum5.toFixed(5);
                     tr[tr.length-1].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value=sum6.toFixed(5);
                     tr[tr.length-1].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=sum7.toFixed(5);
                     tr[tr.length-1].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value=sum8.toFixed(5);
                     tr[tr.length-1].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=sum9.toFixed(5);
                     break;
                }
                case 1:
                {
                    var a1;
                    var t1;
                    for( i=4;i <tr.length-1;i++) 
                    { 
                       if(pattem.test(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim();
                            t1=parseFloat(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            t1=0; 
                            tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
                        }
                        if(pattem.test(tr[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value.Trim();
                            a1=t1*parseFloat(tr[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            a1=0; 
                            tr[i].getElementsByTagName("td")[16].getElementsByTagName("input")[0].value="";
                        }
                         sum1+=a1;
                     }
                     tr[tr.length-1].getElementsByTagName("td")[1].getElementsByTagName("input")[0].value=sum1.toFixed(5);
                     break;
                 }
                 case 2:
                 {
                     var a2;
                     var t2;
                     for( i=4;i <tr.length-1;i++) 
                     {
                      if(pattem.test(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim();
                            t2=parseFloat(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            t2=0; 
                            tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
                        }
                        if(pattem.test(tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value.Trim())) 
                        {
                            tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value.Trim();
                            a2=t2*parseFloat(tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            a2=0; 
                            tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value="";
                        }
                         sum2+=a2;
                     }
                     tr[tr.length-1].getElementsByTagName("td")[2].getElementsByTagName("input")[0].value=sum2.toFixed(5);
                     break;
                 }
                 case 3:
                 {
                    var a3;
                    var t3;
                    for( i=4;i <tr.length-1;i++) 
                    {
                         if(pattem.test(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim();
                            t3=parseFloat(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            t3=0; 
                            tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
                        }
                         if(pattem.test(tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value.Trim()))
                        { 
                            tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value.Trim();
                            a3=t3*parseFloat(tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value); 
                        }
                        else 
                        {
                            a3=0; 
                            tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value="";
                        }
                        sum3+=a3;
                       }
                        tr[tr.length-1].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value=sum3.toFixed(5);
                        break;
                 }
                 case 4:
                 {
                      var a4;
                      var t4;
                      for( i=4;i <tr.length-1;i++) 
                      {
                           if(pattem.test(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim()))
                            { 
                                tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim();
                                t4=parseFloat(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value); 
                            }
                            else 
                            {
                                t4=0; 
                                tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
                            }
                            if(pattem.test(tr[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value.Trim()))
                            { 
                                tr[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value.Trim();
                                a4=t4*parseFloat(tr[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value); 
                            }
                            else 
                            {
                                a4=0; 
                                tr[i].getElementsByTagName("td")[19].getElementsByTagName("input")[0].value="";
                            }
                            sum4+=a4;
                      }
                      tr[tr.length-1].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value=sum4.toFixed(5);
                      break;
                 }
                 case 5:
                 {
                       var a5;
                       var t5;
                       for( i=4;i <tr.length-1;i++) 
                       {
                            if(pattem.test(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim()))
                            { 
                                tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim();
                                t5=parseFloat(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value); 
                            }
                            else 
                            {
                                t5=0; 
                                tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
                            }
                            if(pattem.test(tr[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value.Trim()))
                            { 
                                tr[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value.Trim();
                                a5=t5*parseFloat(tr[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value); 
                            }
                            else 
                            {
                                a5=0; 
                                tr[i].getElementsByTagName("td")[20].getElementsByTagName("input")[0].value="";
                            } 
                              sum5+=a5;
                       }
                       tr[tr.length-1].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value=sum5.toFixed(5);
                       break;
                 }
                 case 6:
                 {
                       var a6;
                       var t6;
                       for( i=4;i <tr.length-1;i++) 
                       {
                            if(pattem.test(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim()))
                            { 
                                tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim();
                                t6=parseFloat(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value); 
                            }
                            else 
                            {
                                t6=0; 
                                tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
                            }
                            if(pattem.test(tr[i].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value.Trim()))
                            { 
                                tr[i].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value.Trim();
                                a6=t6*parseFloat(tr[i].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value); 
                            }
                            else 
                            {
                                a6=0; 
                                tr[i].getElementsByTagName("td")[21].getElementsByTagName("input")[0].value="";
                            }
                            sum6+=a6;
                       }
                        tr[tr.length-1].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value=sum6.toFixed(5);
                        break;
                 }
                 case 7:
                 {
                       var a7;
                       var t7;
                       for( i=4;i <tr.length-1;i++) 
                       {
                            if(pattem.test(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim()))
                            { 
                                tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim();
                                t7=parseFloat(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value); 
                            }
                            else 
                            {
                                t7=0; 
                                tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
                            }
                            if(pattem.test(tr[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value.Trim()))
                            { 
                                tr[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value.Trim();
                                a7=t7*parseFloat(tr[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value); 
                            }
                            else 
                            {
                                a7=0; 
                                tr[i].getElementsByTagName("td")[22].getElementsByTagName("input")[0].value="";
                            } 
                            sum7+=a7;
                       }
                       tr[tr.length-1].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=sum7.toFixed(5);
                       break;
                 }
                 case 8:
                 {
                       var a8;
                       var t8;
                       for( i=4;i <tr.length-1;i++) 
                       {
                            if(pattem.test(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim()))
                            { 
                                tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim();
                                t8=parseFloat(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value); 
                            }
                            else 
                            {
                                t8=0; 
                                tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
                            }
                            if(pattem.test(tr[i].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value.Trim()))
                            { 
                                tr[i].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value.Trim();
                                a8=t8*parseFloat(tr[i].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value); 
                            }
                            else 
                            {
                                a8=0; 
                                tr[i].getElementsByTagName("td")[23].getElementsByTagName("input")[0].value="";
                            }
                            sum8+=a8;
                       }
                       tr[tr.length-1].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value=sum8.toFixed(5);
                       break;
                 }
                 case 9:
                 {
                       var a9;
                       var t9;
                       for( i=4;i <tr.length-1;i++) 
                       {
                            if(pattem.test(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim()))
                            { 
                                tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value.Trim();
                                t9=parseFloat(tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value); 
                            }
                            else 
                            {
                                t9=0; 
                                tr[i].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value="0";
                            }
                            if(pattem.test(tr[i].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.Trim()))
                            { 
                                tr[i].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value.Trim();
                                a9=t9*parseFloat(tr[i].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value); 
                            }
                            else 
                            {
                                a9=0; 
                                tr[i].getElementsByTagName("td")[24].getElementsByTagName("input")[0].value="";
                            }
                            sum9+=a9;
                       }
                        tr[tr.length-1].getElementsByTagName("td")[9].getElementsByTagName("input")[0].value=sum9.toFixed(5);
                        
                        break;
                 }
            }
    
//            for( i=4;i <tr.length-1;i++) 
//            { 
//                var a1,a2,a3,a4,a5,a6,a7,a8,a10; 
//                if(pattem.test(tr[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value.Trim()))
//                { 
//                    tr[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value.Trim();
//                    a1=parseFloat(tr[i].getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML)*parseFloat(tr[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value); 
//                }
//                else 
//                {
//                    a1=0; 
//                    tr[i].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=0;
//                }
//                if(pattem.test(tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value.Trim())) 
//                {
//                    tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value.Trim();
//                    a2=parseFloat(tr[i].getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML)*parseFloat(tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value); 
//                }
//                else 
//                {
//                    a2=0; 
//                    tr[i].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value=0;
//                }
//               if(pattem.test(tr[i].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value.Trim()))
//                { 
//                    tr[i].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value.Trim();
//                    a3=parseFloat(tr[i].getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML)*parseFloat(tr[i].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value); 
//                }
//                else 
//                {
//                    a3=0; 
//                    tr[i].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=0;
//                }
//               if(pattem.test(tr[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.Trim()))
//                { 
//                    tr[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value.Trim();
//                    a4=parseFloat(tr[i].getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML)*parseFloat(tr[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value); 
//                }
//                else 
//                {
//                    a4=0; 
//                    tr[i].getElementsByTagName("td")[11].getElementsByTagName("input")[0].value=0;
//                }
//               if(pattem.test(tr[i].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.Trim()))
//                { 
//                    tr[i].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value.Trim();
//                    a5=parseFloat(tr[i].getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML)*parseFloat(tr[i].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value); 
//                }
//                else 
//                {
//                    a5=0; 
//                    tr[i].getElementsByTagName("td")[12].getElementsByTagName("input")[0].value=0;
//                }  
//               if(pattem.test(tr[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value.Trim()))
//                { 
//                    tr[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value.Trim();
//                    a6=parseFloat(tr[i].getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML)*parseFloat(tr[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value); 
//                }
//                else 
//                {
//                    a6=0; 
//                    tr[i].getElementsByTagName("td")[13].getElementsByTagName("input")[0].value=0;
//                }
//                   
//                if(pattem.test(tr[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value.Trim()))
//                { 
//                    tr[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value.Trim();
//                    a7=parseFloat(tr[i].getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML)*parseFloat(tr[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value); 
//                }
//                else 
//                {
//                    a7=0; 
//                    tr[i].getElementsByTagName("td")[14].getElementsByTagName("input")[0].value=0;
//                }
//                   
//               if(pattem.test(tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value.Trim()))
//                { 
//                    tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value.Trim();
//                    a8=parseFloat(tr[i].getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML)*parseFloat(tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value); 
//                }
//                else 
//                {
//                    a8=0; 
//                    tr[i].getElementsByTagName("td")[17].getElementsByTagName("input")[0].value=0;
//                }
//            if(pattem.test(tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value.Trim()))
//                { 
//                    tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value=tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value.Trim();
//                    a10=parseFloat(tr[i].getElementsByTagName("td")[5].getElementsByTagName("span")[0].innerHTML)*parseFloat(tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value); 
//                }
//                else 
//                {
//                    a10=0; 
//                    tr[i].getElementsByTagName("td")[18].getElementsByTagName("input")[0].value=0;
//                }
//               sum1+=a1;
//               sum2+=a2;
//               sum3+=a3;
//               sum4+=a4;
//               sum5+=a5;
//               sum6+=a6;
//               sum7+=a7;
//               sum8+=a8;
//               sum9+=a10;
//               }
//               tr[tr.length-1].getElementsByTagName("td")[1].getElementsByTagName("input")[0].value=sum1;
//               tr[tr.length-1].getElementsByTagName("td")[2].getElementsByTagName("input")[0].value=sum2;
//               tr[tr.length-1].getElementsByTagName("td")[3].getElementsByTagName("input")[0].value=sum3;
//               tr[tr.length-1].getElementsByTagName("td")[4].getElementsByTagName("input")[0].value=sum4;
//               tr[tr.length-1].getElementsByTagName("td")[5].getElementsByTagName("input")[0].value=sum5;
//               tr[tr.length-1].getElementsByTagName("td")[6].getElementsByTagName("input")[0].value=sum6;
//               tr[tr.length-1].getElementsByTagName("td")[7].getElementsByTagName("input")[0].value=sum7;
//               tr[tr.length-1].getElementsByTagName("td")[8].getElementsByTagName("input")[0].value=sum8;
//               tr[tr.length-1].getElementsByTagName("td")[10].getElementsByTagName("input")[0].value=sum9;
            } 