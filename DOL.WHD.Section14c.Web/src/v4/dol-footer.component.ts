import { Component } from '@angular/core';

@Component({
  selector: 'dol-footer',
  styleUrls: ['./dol-footer.component.css'],
  template: `
    <footer class="dol-footer">
      <div class="dol-footer--brand">
        <a class="brand" href="/">
          <img
            class="brand--img"
            src="images/dol-logo-white.png"
            alt="United States Department of Labor"
            width="70"
            height="70"
          />
          <div class="brand--text">
            UNITED STATES<br>DEPARTMENT OF LABOR
          </div>
        </a>
      </div>
      <div class="dol-footer--content">
        <div class="usa-grid-full">
          <div class="usa-width-one-fourth content-group">
            200 Constitution Ave. NW<br>
            Washington DC 20210<br>
            <a href="tel:1-866-487-2365">1-866-4-USA-DOL</a><br>
            <a href="tel:1-866-487-2365">1-866-487-2365</a><br>
            <a href="http://www.dol.gov/dol/contact/contact-phonecallcenter.htm">TTY</a><br>
            <a href="http://www.dol.gov/">www.dol.gov </a>
          </div>
          <ul class="usa-unstyled-list usa-width-one-fourth content-group" role="navigation">
            <li class="mb1"><strong>ABOUT THE SITE</strong></li>
            <li><a href="http://www.dol.gov/dol/foia/">Freedom of Information Act</a></li>
            <li><a href="http://www.dol.gov/dol/privacynotice.htm">Privacy &amp; Security Statement</a></li>
            <li><a href="http://www.dol.gov/dol/disclaim.htm">Disclaimers</a></li>
            <li><a href="http://www.dol.gov/dol/aboutdol/website-policies.htm">Important Web Site Notices</a></li>
            <li><a href="http://www.dol.gov/dol/aboutdol/plug-ins.htm">Plug-ins Used by DOL</a></li>
            <li><a href="http://www.dol.gov/rss/">RSS Feeds from DOL</a></li>
            <li><a href="http://www.dol.gov/dol/aboutdol/accessibility.htm">Accessibility Statement</a></li>
          </ul>
          <ul class="usa-unstyled-list usa-width-one-fourth content-group" role="navigation">
            <li class="mb1"><strong>LABOR DEPARTMENT</strong></li>
            <li><a href="http://www.dol.gov/dol/topic/Spanish-speakingTopic.htm">Español</a></li>
            <li><a href="http://www.dol.gov/dol/siteindex.htm">A to Z Index</a></li>
            <li><a href="http://www.dol.gov/dol/organization.htm">Agencies</a></li>
            <li><a href="http://www.oig.dol.gov/">Office of Inspector General</a></li>
            <li><a href="http://www.dol.gov/dol/contact/contact-phonekeypersonnel.htm">Leadership Team</a></li>
            <li><a href="http://www.dol.gov/dol/contact/">Contact Us</a></li>
            <li><a href="https://public.govdelivery.com/accounts/USDOL/subscriber/new?topic_id=USDOL_167" onclick="window.open('https://public.govdelivery.com/accounts/USDOL/subscriber/new?topic_id=USDOL_167&pop=t', '', 'scrollbars=1,toolbar=0,menubar=0,resizable=1,width=800,height=420');return false;">Subscribe to the DOL Newsletter</a></li>
            <li><a href="http://www.dol.gov/_sec/newsletter/">Read The DOL Newsletter</a></li>
            <li><a href="http://www.dol.gov/easl/">Emergency Accountability Status Link</a></li>
          </ul>
          <ul class="usa-unstyled-list usa-width-one-fourth content-group" role="navigation">
            <li class="mb1"><strong>FEDERAL GOVERNMENT</strong></li>
            <li><a href="http://www.dol.gov/cgi-bin/leave-dol.asp?exiturl=http://www.whitehouse.gov&exitTitle=www.whitehouse.gov&fedpage=yes">White House</a></li>
            <li><a href="http://www.dol.gov/cgi-bin/leave-dol.asp?exiturl=https://www.healthcare.gov&exitTitle=www.healthcare.gov&fedpage=yes">Affordable Care Act</a></li>
            <li><a href="http://www.dol.gov/opa/disaster-recovery.htm">Disaster Recovery Assistance</a></li>
            <li><a href="http://www.dol.gov/cgi-bin/leave-dol.asp?exiturl=http://www.usa.gov&exitTitle=www.usa.gov&fedpage=yes">USA.gov</a></li>
            <li><a href="http://www.dol.gov/dol/PlainWriting/">Plain Writing Act</a></li>
            <li><a href="http://www.dol.gov/recovery/">Recovery Act</a></li>
            <li><a href="http://www.dol.gov/oasam/programs/crc/NoFearResult.htm">No Fear Act</a></li>
            <li><a href="http://www.dol.gov/cgi-bin/leave-dol.asp?exiturl=https://osc.gov/&exitTitle=osc.gov&fedpage=yes">U.S. Office of Special Counsel</a></li>
          </ul>
        </div>
      </div>
    </footer>
  `
})
export class DolFooterComponent {}
